using AutoMapper;
using DAL.DTO;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Profiles
{
    public class JubsProfiles : Profile
    {
        public JubsProfiles()
        {
            CreateMap<JobDTO, Job>();
            CreateMap<Job, JobDTO>();
        }
    }
}
