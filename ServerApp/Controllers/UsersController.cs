using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Repo;

namespace ServerApp.Controllers
{
    [ApiController]
    public class UsersController:Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public UsersController(IStudentRepository studentRepository,IMapper mapper)
        {
            _studentRepository=studentRepository;
            _mapper=mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllUsers(){
            var users = await _studentRepository.GetAllUserAsync();
            return Ok(_mapper.Map<List<UserForListDTO>>(users));
        }

    }
}