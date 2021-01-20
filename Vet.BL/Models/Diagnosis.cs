using System;
using System.Collections.Generic;
using System.Text;
using VetAmbulance.DAL.Mappers;

namespace VetAmbulance.BL.Models
{
    public class Diagnosis
    {
        private readonly DiagnosisMapper diagnosisMapper;

        public Diagnosis()
        {
        }

        public Diagnosis(DiagnosisMapper diagnosisMapper)
        {
            this.diagnosisMapper = diagnosisMapper;
        }

        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public DateTime Date { get; set; }

        public string DisName { get; set; }

        public string Symptoms { get; set; }

        public string Therapy { get; set; }

        public Patient Patient1
        {
            get => default;
            set
            {
            }
        }

        public Diagnosis GetById(int id)
        {
            var diagnosis = diagnosisMapper.GetById(id);

            if (diagnosis == null)
            {
                return null;
            }

            Id = diagnosis.Id;
            Date = diagnosis.Date;
            PatientId = diagnosis.PatientId;
            DisName = diagnosis.DisName;
            Symptoms = diagnosis.Symptoms;
            Therapy = diagnosis.Therapy;

            return this;
        }

        public List<Diagnosis> GetAll()
        {
            var diagnoses = diagnosisMapper.GetAll();

            var DiagnosesList = new List<Diagnosis>();

            if (diagnoses == null)
            {
                return null;
            }

            foreach (var diagnosis in diagnoses)
            {
                DiagnosesList.Add(new Diagnosis()
                {
                    Id = diagnosis.Id,
                    Date = diagnosis.Date,
                    PatientId = diagnosis.PatientId,
                    DisName = diagnosis.DisName,
                    Symptoms = diagnosis.Symptoms,
                    Therapy = diagnosis.Therapy
                });
            }

            return DiagnosesList;
        }

        public bool Insert(DiagnosisDTO diagnosisDto)
        {
            try
            {
                var diagnosis = new DAL.Diagnosis()
                {
                    PatientId = diagnosisDto.PatientId,
                    Date = diagnosisDto.Date,
                    DisName = diagnosisDto.DisName,
                    Symptoms = diagnosisDto.Symptoms,
                    Therapy = diagnosisDto.Therapy
                };

                diagnosisMapper.Insert(diagnosis);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(DiagnosisDTO DiagnosisDto, int id)
        {
            try
            {
                var Diagnosis = new DAL.Diagnosis()
                {
                    Id = id,
                    PatientId = DiagnosisDto.PatientId,
                    Date = DiagnosisDto.Date,
                    DisName = DiagnosisDto.DisName,
                    Symptoms = DiagnosisDto.Symptoms,
                    Therapy = DiagnosisDto.Therapy
                };

                diagnosisMapper.Edit(Diagnosis);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int id)
        {
            diagnosisMapper.Delete(id);
        }
    }
}
