using Ardalis.Specification;
using Template.Domain.Models;

namespace Template.Domain.Specs
{
    public static class TemplateUserSpec
    {
        public class GetByEmail : Specification<TemplateUser>
        {
            public GetByEmail(string email)
            {
                Query.Where(user => user.TxEmail == email);
            }
        }

        public class GetByCpf : Specification<TemplateUser>
        {
            public GetByCpf(string cpf)
            {
                Query.Where(user => user.TxCpf == cpf);
            }
        }

        public class GetByPhone : Specification<TemplateUser>
        {
            public GetByPhone(string phone)
            {
                Query.Where(user => user.TxPhone == phone);
            }
        }

        public class GetByName : Specification<TemplateUser>
        {
            public GetByName(string name)
            {
                Query.Where(user => user.TxName == name);
            }
        }

        public class GetByIdList : Specification<TemplateUser>
        {
            public GetByIdList(int[] id)
            {
                Query.Where(user => id.Contains(user.IdUser));
            }
        }

        public class GetById : Specification<TemplateUser>
        {
            public GetById(int id)
            {
                Query.Where(user => user.IdUser == id);
            }
        }

        public class GetByPhoneOrEmail : Specification<TemplateUser>
        {
            public GetByPhoneOrEmail(string phone, string email)
            {
                Query.Where(user => user.TxPhone == phone || user.TxEmail == email);
            }
        }


        public class GetByFilters : Specification<TemplateUser>
        {
            public GetByFilters(string phone, string email, string name, HashSet<string> profiles)
            {
                Query.Where(user => (string.IsNullOrEmpty(phone) || user.TxPhone == phone)
                                   && (string.IsNullOrEmpty(email) || user.TxEmail == email)
                                   && (string.IsNullOrEmpty(name) || user.TxName == name)
                                   && (profiles.Count == 0 || profiles.Any(x => profiles.Any(y => y == x))));
            }
        }
    }
}