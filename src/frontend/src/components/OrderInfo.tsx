import React from "react";
import OrderModel from "../models/OrderModel";
import { apiUrl } from "../contexts/ConfigContext";
import useGetOrderSum from "../hooks/useGetOrderSum";

type orderProps = {
  orderInfo: OrderModel;
}

export default function OrderInfo({ orderInfo }: orderProps) {
  const [price, setPrice] = useGetOrderSum(apiUrl, localStorage.getItem('jwt')!, orderInfo.id);
  
  return (
      <div className="orders-history-text-info">
        <p>Номер заказа: {orderInfo.id}</p>
        <p>Стоимость: {price.toFixed(2)} &#8381;</p>
        <p>Статус: {orderInfo.status}</p>
      </div>
  );
}
