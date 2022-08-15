﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : Controller
    {
        [HttpPost]
        public object AddNewSelections([FromBody] Settings settings)
        {
            SettingsProvider settingsProvider = new SettingsProvider();
            var result = settingsProvider.AddNewSettings(settings);
            return result;
        }

        [HttpPost]
        public object UpdateSelections([FromBody] Settings settings)
        {
            SettingsProvider settingsProvider = new SettingsProvider();
            var result = settingsProvider.UpdateSettings(settings);
            return result;
        }

        [HttpGet]
        public IEnumerable<Settings> GetSelectionsById(int BusinessId)
        {
            SettingsProvider settingsProvider = new SettingsProvider();
            var result = settingsProvider.GetSettingsById(BusinessId);
            return result;
        }

    }
}