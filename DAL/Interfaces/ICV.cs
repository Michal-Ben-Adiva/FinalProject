using DAL.DTO;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DAL.Interfaces
{
    public interface ICV
    {
        Task<bool> CreateCV(CVDTO c);
        Task<bool> DeleteCV(long id);
        Task<CV> GetCVByID(long id);
        Task<IEnumerable<CV>> GetAllCV();
        Task<bool> UpdateCV(long id, CVDTO updatecv);
    }
}
