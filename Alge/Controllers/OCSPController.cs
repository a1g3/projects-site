﻿using System;
using System.Net;
using System.Text.RegularExpressions;
using Alge.Domain.Interfaces.Facades;
using Alge.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Alge.Controllers
{
    public class OcspController : Controller
    {
        public IOcspFacade OcspFacade { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Test(string hostname)
        {
            OcspStatusViewModel viewModel;
            if (!String.IsNullOrEmpty(hostname))
            {
                hostname = Regex.Replace(hostname, "(?i)(https://|http://)", String.Empty);
                if(hostname.Contains("/"))
                    hostname = hostname.Substring(0, hostname.IndexOf("/"));
                hostname = WebUtility.UrlEncode(hostname);
                var status = OcspFacade.GetStatus(hostname, 443);
                viewModel = new OcspStatusViewModel()
                    {
                        Status = (int)status.Status,
                        ThisUpdate = status.ThisUpdate,
                        NextUpdate = status.NextUpdate,
                        ProducedAt = status.ProducedAt,
                        RevocationReason = status.RevocationReason,
                        RevocationTime = status.RevocationTime,
                        Certificate = status.Certificate
                    };
            } else
                viewModel = new OcspStatusViewModel() { Status = -1, Error = "Please enter a valid hostname!" };

            return View("Result",viewModel);
        }
    }
}