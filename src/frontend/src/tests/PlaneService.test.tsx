import PlaneService from '../services/PlaneService';
import { expect } from '@jest/globals';
import { apiUrl } from '../contexts/ConfigContext';

describe('Test PlaneService', () => {
  const planeService: PlaneService = new PlaneService(apiUrl, '');
  
  it('Test 1: get all planes', async () => {
    return planeService.getAll()
           .then(res => res.json())
           .then(res => {
            expect(res).not.toBeNull();
           });
  });

  it('Test 2: get plane for flight', async () => {
    return planeService.getByFlightId('2')
           .then(res => res.json())
           .then(res => {
            expect(res).not.toBeNull();
           });
  });

  it('Test 3: get plane for non-existing flight', async () => {
    return planeService.getByFlightId('0')
           .then(res => {
              expect(res.status).toBe(404);
           });
  });

  it('Test 4: get plane', async () => {
    return planeService.getById('2')
           .then(res => res.json())
           .then(res => {
              expect(res).not.toBeNull();
           });
  });

  it('Test 5: get non-existing plane', async () => {
    return planeService.getById('0')
           .then(res => {
              expect(res.status).toBe(404);
           });
  });
});
