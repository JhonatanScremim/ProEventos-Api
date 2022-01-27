using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Interfaces;
using ProEventos.Application.ViewModels;
using ProEventos.Domain;
using ProEventos.Repository.Interfaces;

namespace ProEventos.Application
{
    public class BatchService : IBatchService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IMapper _mapper;

        public BatchService(IBaseRepository baseRepository, IBatchRepository batchRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _batchRepository = batchRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BatchViewModel>> SaveBathesAsync(int eventId, List<BatchViewModel> models)
        {
            var batches = await _batchRepository.GetBatchesByEventIdAsync(eventId);

            if(batches == null || !batches.Any())
                return null;

            foreach(var model in models)
            {
                model.EventId = eventId;

                if(model.Id == default)
                {
                    _baseRepository.Create<Batch>(_mapper.Map<Batch>(model));
                    
                    await _baseRepository.SaveChangesAsync();
                    continue;
                }

                _baseRepository.Update<Batch>(_mapper.Map<Batch>(model));
                await _baseRepository.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<BatchViewModel>>
                (await _batchRepository.GetBatchesByEventIdAsync(eventId));
        }

        public async Task<bool> DeleteAsync(int eventId, int batchId)
        {
            var batch = await _batchRepository.GetBatchByIdsAsync(eventId, batchId);

            if(batch == null)
                throw new Exception("Batch not find");

            _baseRepository.Delete<Batch>(batch); 
            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<BatchViewModel> GetBatchByIdsAsync(int eventId, int batchId)
        {
            var response = await _batchRepository.GetBatchByIdsAsync(eventId, batchId);

            if(response == null)
                return null;

            return _mapper.Map<BatchViewModel>(response);
        }

        public async Task<IEnumerable<BatchViewModel>> GetBatchesByEventIdAsync(int eventId)
        {
            var batches = await _batchRepository.GetBatchesByEventIdAsync(eventId);
            
            if(batches == null)
                return null;
                
            return _mapper.Map<IEnumerable<BatchViewModel>>(batches);
        }
    }
}