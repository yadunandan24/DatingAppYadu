﻿using API.DTO_s;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context,IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            /*
            return await _context.Users.
                Where(x => x.UserName == username).
                Select(user => new MemberDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    KnownAs= user.KnownAs,
                    //.....
                    //.....
                    //.....
                }).SingleOrDefaultAsync();
            */ //without use of automapper

            return await _context.Users.
                Where(x => x.UserName == username).
                ProjectTo<MemberDto>(_mapper.ConfigurationProvider).  //function of automapper
                SingleOrDefaultAsync();

        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users.
                 ProjectTo<MemberDto>(_mapper.ConfigurationProvider).
                 ToListAsync(); 

        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.
                Include(p => p.Photos).
                SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.
                Include(p => p.Photos).  //to also get the related entities data from the db, otherwise only data from users table is returned
                ToListAsync();           //and the photos array for a particular user is returned as empty

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified; //just informing ef tracker that change is made to user we passed
        }
    }
}
