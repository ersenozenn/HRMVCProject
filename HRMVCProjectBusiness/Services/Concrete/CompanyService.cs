﻿using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectDataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Concrete
{
    public class CompanyService : GenericService<Company>, ICompanyService
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository) : base(companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public Company GetByIdIncludeEmployees(int id)
        {
            return companyRepository.GetByIdWithEmployees(id);
        }
    }
}
