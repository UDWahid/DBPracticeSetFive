using DOOR.Client.Services.Common;
using DOOR.Shared.DTO;

namespace DOOR.Client.Services.Core
{
    public class CourseService : BaseService<CourseDTO>
    {
        public CourseService(HttpClient client) 
            : base(client, "Course")
        {
        }
    }
}
