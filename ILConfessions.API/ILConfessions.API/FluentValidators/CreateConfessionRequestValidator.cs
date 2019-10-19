using FluentValidation;
using ILConfessions.API.Contracts.V1.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.FluentValidators
{
    public class CreateConfessionRequestValidator : AbstractValidator<CreateConfessionRequest>
    {
        public CreateConfessionRequestValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
            RuleFor(r => r.Description)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
