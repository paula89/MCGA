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
    public class PrivilegesBLL
    {
        private PrivilegeService _repo;
        private RefreshTokenService _repoRT;
        private UserPrivilegeService _repoUP;
        private string _token;

        public PrivilegesBLL(string token)
        {
            _token = token;
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repo = new PrivilegeService();
            _repoRT = new RefreshTokenService();
            _repoUP = new UserPrivilegeService();
        }

        public async Task<List<PrivilegeListResponse>> GetAllAsync()
        {
            await CheckTokenAsync("GETPRIVILEGES");
            return (await _repo.DisplayAll()).Select(x => new PrivilegeListResponse(x)).ToList();
        }

        public async Task<PrivilegeResponse> GetAsync(int id)
        {
            await CheckTokenAsync("GETPRIVILEGES");
            return new PrivilegeResponse(await _repo.Get(id));
        }

        public async void Create(CreatePrivilegeRequest request)
        {
            await CheckTokenAsync("CREATEPRIVILEGES");
            await _repo.Add(request.GetPrivilege());
        }

        public async Task UpdateAsync(UpdatePrivilegeRequest request)
        {
            await CheckTokenAsync("UPDATEPRIVILEGES");
            await _repo.Update(request.GetPrivilege());
        }

        public async Task DeleteAsync(int id)
        {
            await CheckTokenAsync("DELETEPRIVILEGES");
            await _repo.Delete(id);
        }

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
                (await _repo.GetByDescription(description)).Id)
                ) == null)
            {
                throw new Exception();
            }
        }
    }
}
