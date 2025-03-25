import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../contexts/AuthContext";
import UserService from "../services/UserService";
import { apiUrl } from "../contexts/ConfigContext";

export default function Settings () {
  const [email, setEmail] = useState('');
  const [oldPassword, setOldPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [formErr, setFormErr] = useState('');

  const submitSettings = (e: any) => {
    e.preventDefault();

    const userService: UserService = new UserService(apiUrl, localStorage.getItem('jwt')!);

    userService.changeSettings(email, oldPassword, newPassword, firstName, lastName)
    .then(res => {
      if (res.ok) {
        setFormErr('Данные успешно обновлены');
        return res.json();
      }
      else {
        throw res.status;
      }
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
    <div className="settings-form">
      <ul>
        <h1 className="page-title">Настройки</h1>
        <form onSubmit={submitSettings}>
          <li>
            <input className="text-box" name="email" value={email} onChange={(e) => setEmail(e.target.value)} 
              placeholder="Адрес электронной почты"></input>
          </li>
          <li>
            <input className="text-box" name="oldPassword" value={oldPassword} onChange={(e) => setOldPassword(e.target.value)} 
              type="password" placeholder="Старый пароль"></input>
          </li>
          <li>
            <input className="text-box" name="newPassword" value={newPassword} onChange={(e) => setNewPassword(e.target.value)} 
              type="password" placeholder="Новый пароль"></input>
          </li>
          <li>
            <input className="text-box" name="firstName" value={firstName} onChange={(e) => setFirstName(e.target.value)} 
              placeholder="Имя"></input>
          </li>
          <li>
            <input className="text-box" name="lastName" value={lastName} onChange={(e) => setLastName(e.target.value)} 
              placeholder="Фамилия"></input>
          </li>
          <li>
            <button className="big-button" type="submit">Подтвердить</button>
          </li>
        </form>
        <li>
          {formErr === '' ? <></> : <p className="error-message">{formErr}</p>}
        </li>
      </ul>
    </div>
  )
}
