import { useState } from "react";
import { useNavigate } from "react-router-dom";
import UserService from "../services/UserService";
import { apiUrl } from "../contexts/ConfigContext";
import './Signup.css';

export default function Signup () {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [formErr, setFormErr] = useState('');

  let navigate = useNavigate();

  const handleSubmit = (e: any) => {
    e.preventDefault();

    const userService: UserService = new UserService(apiUrl, '');

    userService.signup(email, password)
    .then(res => {
      if (res.ok) {
        return res.json();
      }
      else {
        throw res.status;
      }
    })
    .then(res => {
      navigate('/users/login');
    })
    .catch(err => {
      if (err === 409) {
        setFormErr('Ошибка: адрес электронной почты уже используется');
        return;
      }
      else {
        setFormErr('Ошибка сервера');
        return;
      }
    });
  }

  return (
    <div className="signup-form">
      <ul>
        <li>
          <h1 className="page-title">Регистрация</h1>
        </li>
        <form onSubmit={handleSubmit}>
        <li><input className="text-box" name="email" value={email} onChange={(e) => setEmail(e.target.value)} 
          placeholder="Адрес электронной почты" minLength={3}></input></li>
        <li><input className="text-box" name="password" value={password} onChange={(e) => setPassword(e.target.value)} 
          type="password" placeholder="Пароль" minLength={6}></input></li>
        <li><button className="big-button" type="submit">Зарегистрироваться</button></li>
        </form>
        <li>
          {formErr === '' ? <></> : <p className="error-message">{formErr}</p>}
        </li>
      </ul>
    </div>
  )
}
