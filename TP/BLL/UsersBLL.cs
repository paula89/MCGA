 using DAL;
using Helpers;
using Helpers.CustomExeptions;
using Helpers.Request;
using Helpers.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class UsersBLL
    {
        private UserService _repo;
        private RefreshTokenService _repoRT;
        private PrivilegeService _repoP;
        private UserPrivilegeService _repoUP;
        private string _token;

        public UsersBLL()
        {
            InicializeRepos();
        }

        public UsersBLL(string token)
        {
            _token = token;
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repo = new UserService();
            _repoRT = new RefreshTokenService();
            _repoP = new PrivilegeService();
            _repoUP = new UserPrivilegeService();
        }

        public async Task<LogInResponse> LogIn(LogInRequest request)
        {
            var user = await _repo.GetByUserAndPassword(request.UserName, request.Password);
            if (user == null)
            {
                throw new EntityDontExistException();
            }
            var token = GenerateNewToken(user);
            await _repoRT.Add(token);

            return new LogInResponse(user, token, await GetPrivileges(user.Id));
        }

        public async Task<List<UserListResponse>> GetAllAsync()
        {
            await CheckTokenAsync("GETUSERS");
            return (await _repo.DisplayAll()).Select(x => new UserListResponse(x)).ToList();
        }

        public async Task<UserResponse> GetAsync(int id)
        {
            await CheckTokenAsync("GETUSERS");
            return new UserResponse(await _repo.Get(id), await GetPrivileges(id));
        }

        public async void Create(CreateUserRequest request)
        {
            await CheckTokenAsync("CREATEUSERS");
            await _repo.Add(request.GetUser());
            UpdatePrivilege((await _repo.GetByUser(request.Username)).Id, request.Privileges);
        }

        public async Task UpdateAsync(UpdateUserRequest request)
        {
            await CheckTokenAsync("UPDATEUSERS");
            await _repo.Update(request.GetUser());
            UpdatePrivilege(request.Id, request.Privileges);
        }

        public async Task DeleteAsync(int id)
        {
            await CheckTokenAsync("DELETEUSERS");
            await _repo.Delete(id);
        }

        private void UpdatePrivilege(int userId, List<Privilege> privileges) => privileges.ForEach(async x => await _repoUP.Add(new UserPrivilege() { UserId = userId, PrivilegeId = x.Id }));

        private async Task CheckTokenAsync(string description)
        {
            var token = await _repoRT.GetByToken(_token);
            if (token == null || DateTime.Now > token.Expires)
            {
                throw new ExpiredOrInvalidTokenException();
            }
            await CheckPrivilegesAsync(token, description);
        }

        private async Task CheckPrivilegesAsync(RefreshToken token, string description)
        {
            if ((await _repoUP.GetUserPrivilege(
                token.UserId,
                (await _repoP.GetByDescription(description)).Id)
                ) == null)
            {
                throw new Exception();
            }
        }

        private RefreshToken GenerateNewToken(User user) => new RefreshToken()
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            Expires = DateTime.Now.AddHours(2)
        };

        private async Task<List<Privilege>> GetPrivileges(int userId)
        {
            var privileges = new List<Privilege>();
            foreach (var entity in (await _repoUP.GetPrivilegesByUserId(userId)))
            {
                privileges.Add(await _repoP.Get(entity.PrivilegeId));
            }

            return privileges;
        }
    }
}
