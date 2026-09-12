using CMACMaynas.Web.SIRO.Seguridad.Auth.Interfraces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CMACMaynas.Web.SIRO.Seguridad.Auth.Services
{
    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength => throw new NotImplementedException();

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("El valor no puede ser nulo o vacío.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("El valor no puede ser nulo o vacío.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("El valor no puede ser nulo o vacío.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("El valor no puede ser nulo o vacío.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("El valor no puede ser nulo o vacío.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("El valor no puede ser nulo o vacío.", "email");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("El valor no puede ser nulo o vacío.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("El valor no puede ser nulo o vacío.", "password");

            return _provider.ValidateUser(userName, password);
        }
    }
}
