import UserService from '../services/UserService';
import { expect } from '@jest/globals';
import { apiUrl } from '../contexts/ConfigContext';

describe('Test UserService', () => {
  const userService: UserService = new UserService(apiUrl, '');
  
  it('Test 1: login with empty data', async () => {
    return userService.login('', '')
           .then(res => {
              expect(res.status).toBe(404);
          });
  });

  it('Test 2: login with correct data', async () => {
    return userService.login('test@gmail.com', '111111')
           .then(res => res.json())
           .then(res => {
              expect(res).toHaveProperty('jwt');
          });
  });

  it('Test 3: login with incorrect data', async () => {
    return userService.login('test@gmail.com', '222222')
           .then(res => {
              expect(res.status).toBe(400);
          });
  });

  it('Test 4: sign up with busy email', async () => {
    return userService.signup('test@gmail.com', '333333')
           .then(res => {
              expect(res.status).toBe(409);
          });
  });

  it('Test 5: change settings while unauzthorized', async () => {
    return userService.changeSettings('test@gmail.com', '333333', '111111', 'Name', 'Surname')
           .then(res => {
              expect(res.status).toBe(401);
          });
  });
});
