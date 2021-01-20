using System;
using System.Collections.Generic;
using System.Text;
using VetAmbulance.BL.Security;
using VetAmbulance.DAL.Mappers;
namespace VetAmbulance.BL.Models
{
    public class Vet
    {
        private readonly VetMapper vetMapper;

        public Vet()
        {
        }

        public Vet(VetMapper vetMapper)
        {
            this.vetMapper = vetMapper;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }



        public int AmbulanceId { get; set; }
        public Ambulance Ambulance { get; set; }

        public Ambulance Ambulance1
        {
            get => default;
            set
            {
            }
        }

        public Vet GetById(int id)
        {
            var vet = vetMapper.GetById(id);

            if (vet == null)
            {
                return null;
            }

            Id = vet.Id;
            Name = vet.Name;
            PasswordSalt = vet.PasswordSalt;
            PasswordHash = vet.PasswordHash;
            AmbulanceId = vet.AmbulanceId;

            return this;
        }

        public Vet GetByName(string name)
        {
            var vet = vetMapper.GetByName(name);

            if (vet == null)
            {
                return null;
            }

            Id = vet.Id;
            Name = vet.Name;
            PasswordSalt = vet.PasswordSalt;
            PasswordHash = vet.PasswordHash;
            AmbulanceId = vet.AmbulanceId;

            return this;
        }

        public List<Vet> GetAll()
        {
            var vets = vetMapper.GetAll();

            var vetList = new List<Vet>();

            if (vets == null)
            {
                return null;
            }

            foreach (var vet in vets)
            {
                vetList.Add(new Vet()
                {
                    Id = vet.Id,
                    Name = vet.Name,
                    PasswordSalt = vet.PasswordSalt,
                    PasswordHash = vet.PasswordHash,
                    AmbulanceId = vet.AmbulanceId,
                });
            }

            return vetList;
        }

        public bool Insert(VetDTO vetDTO)
        {
            try
            {
                var password = PasswordHelper.CreateHash(vetDTO.Password);

                var vet = new DAL.Vet()
                {
                    Name = vetDTO.Name,
                    PasswordHash = password.PasswordHash,
                    PasswordSalt = password.PasswordSalt,
                    AmbulanceId = vetDTO.AmbulanceId
                };

                vetMapper.Insert(vet);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(VetDTO vetDTO, int id)
        {
            try
            {
                var vet = new DAL.Vet()
                {
                    Id = id,
                    Name = vetDTO.Name,

                    AmbulanceId = vetDTO.AmbulanceId
                };

                if (!string.IsNullOrEmpty(vetDTO.Password))
                {
                    var password = PasswordHelper.CreateHash(vetDTO.Password);

                    vet.PasswordHash = password.PasswordHash;
                    vet.PasswordSalt = password.PasswordSalt;
                }

                vetMapper.Edit(vet);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int id)
        {
            vetMapper.Delete(id);
        }
    }
}
