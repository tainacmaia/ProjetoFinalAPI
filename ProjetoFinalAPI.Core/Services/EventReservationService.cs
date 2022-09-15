﻿using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;

        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public List<EventReservation> GetEventReservation()
        {
            return _eventReservationRepository.GetEventReservation();
        }

        public List<EventReservation> GetEventReservationByPersonNameAndTitle(string personName, string title)
        {
            return _eventReservationRepository.GetEventReservationByPersonNameAndTitle(personName, title);
        }

        public bool InsertEventReservation(EventReservation eventReservation)
        {
            return _eventReservationRepository.InsertEventReservation(eventReservation);
        }
        public bool UpdateEventReservation(long idReservation, EventReservation eventReservation)
        {
            try
            {
                eventReservation = null;
                eventReservation.IdReservation = idReservation;
            }
            catch (Exception ex)
            {
                var tipoExcecao = ex.GetType().Name;
                var mensagem = ex.Message;
                var caminho = ex.InnerException.StackTrace;

                Console.WriteLine($"Tipo da Exceção: {tipoExcecao}, Mensagem: {mensagem}, Stack Trace: {caminho}");
                return false;
            }

            return _eventReservationRepository.UpdateEventReservation(idReservation, eventReservation);
        }
        public bool DeleteEventReservation(long idReservation)
        {
            return _eventReservationRepository.DeleteEventReservation(idReservation);
        }
    }
}