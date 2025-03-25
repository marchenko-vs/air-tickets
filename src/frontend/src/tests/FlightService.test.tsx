import FlightService from '../services/FlightService';
import {expect, jest, test} from '@jest/globals';
import { apiUrl } from '../contexts/ConfigContext';

describe('Test FlightService', () => {
  const flightService: FlightService = new FlightService(apiUrl, '');

  it('Test 1: get all flights', async () => {
    return flightService.getWithFilters(null, null, null)
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 2: get flights with departure point only', async () => {
    return flightService.getWithFilters('Москва', null, null)
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 3: get flights with arrival point only', async () => {
    return flightService.getWithFilters(null, 'Москва', null)
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 4: get flights with departure date only', async () => {
    return flightService.getWithFilters(null, null, '2024-02-15')
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 5: get flights with departure and arrival point only', async () => {
    return flightService.getWithFilters('Москва', 'Санкт-Петербург', null)
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 6: get flights with departure point and departure date only', async () => {
    return flightService.getWithFilters('Москва', null, '2024-02-15')
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 7: get flights with arrival point and arrival date only', async () => {
    return flightService.getWithFilters(null, 'Санкт-Петербург', '2024-02-15')
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 8: get flights with all filters', async () => {
    return flightService.getWithFilters('Москва', 'Санкт-Петербург', '2024-02-15')
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 9: get unique points', async () => {
    return flightService.getUniquePoints()
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });
});
