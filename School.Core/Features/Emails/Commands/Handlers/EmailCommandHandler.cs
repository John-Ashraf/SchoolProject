﻿using MediatR;
using School.Core.Bases;
using School.Core.Features.Emails.Commands.Models;
using School.Services.Abstracts;

namespace School.Core.Features.Emails.Commands.Handlers
{
    public class EmailCommandHandler : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IEmailService _emailsService;
        #endregion
        #region constructor
        public EmailCommandHandler(IEmailService emailService)
        {
            _emailsService = emailService;
        }
        #endregion
        #region HandleFunctions
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailsService.SendEmail(request.Email, request.Message, null);
            if (response == "Success")
                return Success<string>("");
            return BadRequest<string>("Sending Email Failed");
        }
        #endregion

    }
}
