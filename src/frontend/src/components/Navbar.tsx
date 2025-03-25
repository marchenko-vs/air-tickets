import { Component, createContext, useContext, useState } from 'react';
import '../index.css';
import { Link, useMatch, useResolvedPath, Path, Route } from "react-router-dom";
import { AuthContext, AuthContextType } from '../contexts/AuthContext';

export default function Navbar() {
  const authContext = useContext(AuthContext);

  const logOut = () => {
    authContext.setIsLoggedIn(false);
    localStorage.clear();
  }

  return (
    <div className='topnav'>
      <header>
        <nav className='nav-container'>
          <ul className="left-navbar">
            <Link to="/"><img className="site-logo" src='/plane-logo.png' /></Link>
            <CustomLink to="/planes">Самолеты</CustomLink>
            <CustomLink to="/services">Услуги</CustomLink>
          </ul>
          <ul className="right-navbar">
            {
              !authContext.isLoggedIn ? <></> : <CustomLink to="/orders/current">Заказ</CustomLink> 
            }
            {
              !authContext.isLoggedIn ? <CustomLink to="/users/signup">Регистрация</CustomLink> : 
              <CustomLink to="/users/settings">Настройки</CustomLink>
            }
            {
              !authContext.isLoggedIn ? <CustomLink to="/users/login">Вход</CustomLink> : 
              <LogoutLink to="/login" onClick={logOut}>Выход</LogoutLink>
            }  
          </ul>
        </nav>
      </header>
    </div>
  )
}

type linkProps = {
  to: string;
  children: string;
}

function CustomLink({ to, children, ...props }: linkProps) {
  const resolvedPath: Path = useResolvedPath(to);
  const isActive: any = useMatch({ path: resolvedPath.pathname });
  
  return (
      <li className={isActive ? "active" : ""}>
          <Link to={to}>{children}</Link>
      </li>
  )
}

type logoutLinkProps = {
  to: string;
  children: string;
  onClick: () => void
}

function LogoutLink({ to, children, onClick, ...props }: logoutLinkProps) {
  const resolvedPath: Path = useResolvedPath(to);
  const isActive: any = useMatch({ path: resolvedPath.pathname });
  
  return (
      <li className={isActive ? "active" : ""}>
          <Link to={to} onClick={onClick}>{children}</Link>
      </li>
  )
}
