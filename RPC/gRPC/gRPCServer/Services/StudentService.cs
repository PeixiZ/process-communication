using Grpc.Core;
using grpc.stu;
using System.Threading.Tasks;

namespace grpc.stu.Demo;

public class StudentService : StudentLocal.StudentLocalBase
{
    public override Task<Student> Select(Student request, ServerCallContext context)
    {        
        return Task.FromResult(new Student { Age = 30 });        
    }
}