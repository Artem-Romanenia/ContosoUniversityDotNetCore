using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using FluentValidation;
using MediatR;

namespace ContosoUniversity.Features.Students
{
    public class Create
    {
        public class Query : IRequest<Command>
        { }

        public class QueryHandler : AsyncRequestHandler<Query, Command>
        {
            protected override Task<Command> HandleCore(Query request)
            {
                return Task.FromResult(new Command());
            }
        }

        public class Command : IRequest<int>
        {
            public string LastName { get; set; }

            [Display(Name = "First Name")]
            public string FirstMidName { get; set; }

            public DateTime? EnrollmentDate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(m => m.LastName).NotNull().Length(1, 50);
                RuleFor(m => m.FirstMidName).NotNull().Length(1, 50);
                RuleFor(m => m.EnrollmentDate).NotNull();
            }
        }

        public class Handler : AsyncRequestHandler<Command, int>
        {
            private readonly SchoolContext _db;

            public Handler(SchoolContext db) => _db = db;

            protected override async Task<int> HandleCore(Command message)
            {
                var student = Mapper.Map<Command, Student>(message);

                _db.Students.Add(student);

                await _db.SaveChangesAsync();

                return student.Id;
            }
        }
    }
}