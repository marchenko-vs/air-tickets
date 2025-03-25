import OrderService from '../services/OrderService';
import { expect } from '@jest/globals';
import { apiUrl } from '../contexts/ConfigContext';
import TicketModel from '../models/TicketModel';

describe('Test OrderService', () => {
  const orderService: OrderService = new OrderService(apiUrl, '');

  it('Test 1: get tickets for current order', async () => {
    return orderService.getOrderedTickets()
            .then(res => {
              expect(res.status).toBe(401);
            });
  });

  it('Test 2: get price of current order', async () => {
    return orderService.getCurrentSum()
            .then(res => {
              expect(res.status).toBe(401);
            });
  });

  it('Test 3: get price of order', async () => {
    return orderService.getSumByOrderid(3)
            .then(res => {
              expect(res.status).toBe(401);
            });
  });

  it('Test 4: get tickets for order', async () => {
    return orderService.getTicketsByOrderId('3')
            .then(res => {
              expect(res.status).toBe(401);
            });
  });

  it('Test 5: create order', async () => {
    return orderService.create()
            .then(res => {
              expect(res.status).toBe(401);
            });
  });

  it('Test 6: pay order', async () => {
    return orderService.pay()
            .then(res => {
              expect(res.status).toBe(401);
            });
  });

  it('Test 7: get history of orders', async () => {
    return orderService.getHistory()
            .then(res => {
              expect(res.status).toBe(401);
            });
  });
});
