import React from "react";
import TicketModel from "../models/TicketModel";
import { useNavigate } from "react-router-dom";

type ticketProps = {
  ticketInfo: TicketModel;
}

export default function TicketInfo({ ticketInfo }: ticketProps) {
  return (
    <div className="text-info">
      <p>Класс: {ticketInfo.class}</p>
      <p>Ряд: {ticketInfo.row}</p>
      <p>Место: {ticketInfo.place}</p>
      <p>Стоимость: {ticketInfo.price.toFixed(2)} &#8381;</p>
      <p>{ticketInfo.refund ? 'Присутствует' : 'Отсутствует'} возможность возврата</p>
    </div>
  );
}
