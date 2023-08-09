using System;
using Application.Account.DTOs;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Account
{
    public class Register
    {
        public class Command : IRequest<BaseResponse>
        {
            public string NationalIdNumber { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
            public string TeacherNumber { get; set; }
            public string StudentNumber { get; set; }
            public string Title { get; set; }
            public string Salary { get; set; }
            public bool IsTeacher { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.NationalIdNumber).NotEmpty().NotNull().Length(11);
                            //.Custom((x, context) =>
                            //{
                            //    if ((!(long.TryParse(x, out var value)) || value < 0))
                            //    {
                            //        context.AddFailure($"{x} is not a valid number");
                            //    }
                            //});
                RuleFor(x => x.Name).NotEmpty().NotNull();
                RuleFor(x => x.Surname).NotEmpty().NotNull();
                When(x => x.IsTeacher == true, () => {
                    RuleFor(x => x.DateOfBirth).Must(AgeValidate)
                                            .WithMessage("Invalid date age must be 21 or greater than 21");
                    RuleFor(x => x.StudentNumber).Empty();
                    RuleFor(x => x.Title).NotEmpty();
                    RuleFor(x => x.TeacherNumber).NotEmpty().Length(10);
                });
                When(x => x.IsTeacher == false, () => {
                    RuleFor(x => x.DateOfBirth).Must(StudentAgeValidate)
                                            .WithMessage("Invalid date age must be 22 or less than 22");
                    RuleFor(x => x.StudentNumber).NotEmpty().Length(10);
                    RuleFor(x => x.TeacherNumber).Empty();
                });
                RuleFor(x => x.IsTeacher).NotNull();

            }
            private bool AgeValidate(DateTime value)
            {
                DateTime now = DateTime.Today;
                int age = now.Year - Convert.ToDateTime(value).Year;

                if (age < 21)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            private bool StudentAgeValidate(DateTime value)
            {
                DateTime now = DateTime.Today;
                int age = now.Year - Convert.ToDateTime(value).Year;

                if (age > 22 )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        
        public class Handler : IRequestHandler<Command, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<BaseResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var valid = (request.IsTeacher && await _userManager.Users.AnyAsync(x => x.TeacherNumber == request.TeacherNumber))
                            ||
                            (!request.IsTeacher && await _userManager.Users.AnyAsync(x => x.StudentNumber == request.StudentNumber));
                if (!valid)
                {
                    var user = new ApplicationUser
                    {
                        NationalIdNumber = request.NationalIdNumber,
                        Name = request.Name,
                        Surname = request.Surname,
                        DateOfBirth = request.DateOfBirth,
                        TeacherNumber = request.TeacherNumber,
                        StudentNumber = request.StudentNumber,
                        Title = request.Title,
                        Salary = request.Salary,
                        IsTeacher = request.IsTeacher,
                        UserName = request.IsTeacher ? request.TeacherNumber : request.StudentNumber,
                        
                    };

                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.Teacher.ToString());
                        await _userManager.UpdateAsync(user);
                        return new BaseResponse
                        {
                            IsSuccess = true,
                            Message = "Success",
                            Data = new Command
                            {
                                NationalIdNumber = request.NationalIdNumber,
                                Name = request.Name,
                                Surname = request.Surname,
                                DateOfBirth = request.DateOfBirth,
                                TeacherNumber = request.TeacherNumber,
                                StudentNumber = request.StudentNumber,
                                Title = request.Title,
                                Salary = request.Salary,
                                IsTeacher = request.IsTeacher
                            }
                        };
                    }
                    else
                    {
                        string error = result.Errors.FirstOrDefault().Description;
                        //_logger.LogInformation($"Register user terminated [Reason : Creating user failed | Error : {error}]\n");
                        return new BaseResponse
                        {
                            IsSuccess = false,
                            Message = error,
                            Data = null
                        };
                    }
                }
                //_logger.LogInformation($"Register user terminated [Reason : Email/Phonnumber is not unique]\n");
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = request.IsTeacher ? "Teacher already submitted":"Student already submitted",
                    Data = null
                };
            }
        }
        public class BaseResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public Command? Data { get; set; }
        }
    }

}

