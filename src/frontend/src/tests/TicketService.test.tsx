import TicketService from '../services/TicketService';
import { expect } from '@jest/globals';
import { apiUrl } from '../contexts/ConfigContext';

describe('Test TicketService', () => {
  const ticketService: TicketService = new TicketService(apiUrl, '');
  
  it('Test 1: add ticket to order while unauthorized', async () => {
    return ticketService.addToOrder(2)
           .then(res => {
              expect(res.status).toBe(401);
           });
  });

  it('Test 2: get tickets for flight', async () => {
    return ticketService.getByFlightId('5')
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 3: get tickets for non-existing flight', async () => {
    return ticketService.getByFlightId('0')
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });
});
