import ServiceService from '../services/ServiceService';
import { expect } from '@jest/globals';
import { apiUrl } from '../contexts/ConfigContext';

describe('Test ServiceService', () => {
  const serviceService: ServiceService = new ServiceService(apiUrl, '');

  it('Test 1: get all services', async () => {
    return serviceService.getAll()
            .then(res => res.json())
            .then(res => {
              expect(res).not.toBeNull();
            });
  });

  it('Test 2: get active services for ticket', async () => {
    return serviceService.getActiveByTicketId('3')
          .then(res => {
            expect(res.status).toBe(401);
          });
  });

  it('Test 3: get active services for non-existing ticket', async () => {
    return serviceService.getActiveByTicketId('0')
          .then(res => {
            expect(res.status).toBe(401);
          });
  });

  it('Test 4: get inactive services for ticket', async () => {
    return serviceService.getInactiveByTicketId('3', 'бизнес')
          .then(res => {
            expect(res.status).toBe(401);
          });     
  });

  it('Test 5: get active services for non-existing ticket', async () => {
    return serviceService.getInactiveByTicketId('0', 'null')
          .then(res => {
            expect(res.status).toBe(401);
    });
  });

  it('Test 6: remove service from ticket while unauthorized', async () => {
    return serviceService.removeFromTicket('5', '3')
          .then(res => {
            expect(res.status).toBe(401);
          });
  });

  it('Test 7: add service to ticket while unauthorized', async () => {
    return serviceService.addToTicket('5', '3')
          .then(res => {
            expect(res.status).toBe(401);
          });
  });
});
