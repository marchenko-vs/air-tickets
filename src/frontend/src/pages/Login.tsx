import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../contexts/AuthContext";
import UserService from "../services/UserService";
import { apiUrl } from "../contexts/ConfigContext";
import './Login.css';

export default function Login () {
  const authContext = useContext(AuthContext);

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [formErr, setFormErr] = useState('');

  let navigate = useNavigate();

  const handleSubmit = (e: any) => {
    e.preventDefault();

    const userService: UserService = new UserService(apiUrl, '');

    userService.login(email, password)
    .then(res => {
      if (res.ok) {
        return res.json();
      }
      else {
        throw res.status;
      }
    })
    .then(res => {
      localStorage.setItem('jwt', res.jwt);
      authContext.setIsLoggedIn(true);
      navigate('/');
    })
    .catch(err => {
      if (err === 400) {
        setFormErr('Ошибка: неверный пароль');
        return;
      }
      else if (err === 404) {
        setFormErr('Ошибка: пользователь с такой электронной почтой не зарегистрирован');
        return;
      }
      else {
        setFormErr('Ошибка сервера');
        return;
      }
    });
  }
  
  return (
    <div className="login-form">
      <ul>
        <li><h1 className="page-title">Вход</h1></li>
        <form onSubmit={handleSubmit}>
        <li><input className="text-box" name="email" value={email} onChange={(e) => setEmail(e.target.value)}
          minLength={3} placeholder="Адрес электронной почты"></input></li>
        <li><input className="text-box" name="password" value={password} onChange={(e) => setPassword(e.target.value)} 
          minLength={6} type="password" placeholder="Пароль"></input></li>
        <li><button className="big-button" type="submit">Войти</button></li>
        </form>
        <li>
          {formErr === '' ? <></> : <p className="error-message">{formErr}</p>}
        </li>
      </ul>
    </div>
  )
}
