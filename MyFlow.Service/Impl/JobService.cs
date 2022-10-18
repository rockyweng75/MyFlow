﻿using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class JobService : BasicCRUDService<JobDao, Job, JobVM>
    {
        private IJobDao jobDao;

        public override BasicDao<Job> dao {
            get{
                return (BasicDao<Job>)jobDao;
            }
        }

        public JobService(IJobDao jobDao)
        {
            this.jobDao = jobDao;
        }
    }
}
