﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProfessionnelAPI.Data;
using ProfessionnelAPI.Models;

namespace ProfessionnelAPI.Controllers
{
    [Route("api/professionnel/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ProfessionnelContext _context;
        private static string URI_LOGIN = "http://localhost:5000/api/logins";
        private static string URI_CLIENT = "http://localhost:7000/api/client";

        public LoginsController(ProfessionnelContext context)
        {
            _context = context;
        }
        

    // GET: api/professionnel/Logins
    [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogin()
        {
            return null;
        }

        // GET: api/Logins/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetLogin()
        {
            
        }*/

        // PUT: api/Logins/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin(string id, Login login)
        {
            if (id != login.Email)
            {
                return BadRequest();
            }

            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Logins
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login login)
        {
            _context.Login.Add(login);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoginExists(login.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLogin", new { id = login.Email }, login);
        }

        // DELETE: api/Logins/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Login>> DeleteLogin(string id)
        {
            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();

            return login;
        }

        private bool LoginExists(string id)
        {
            return _context.Login.Any(e => e.Email == id);
        }
    }
}
