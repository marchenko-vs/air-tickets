import { useEffect, useState } from "react";
import OrderModel from "../models/OrderModel";
import OrderService from "../services/OrderService";

export type OrdersHookType = [
  orders: OrderModel[], 
  setOrders: (value: any) => void
]

export default function useGetOrdersHistory(baseUrl: string, jwt: string): OrdersHookType {
  const [orders, setOrders] = useState([]);

  const orderService: OrderService = new OrderService(baseUrl, jwt);

  useEffect(() => {
    orderService.getHistory()
    .then(res => {
      if (res.ok) {
        return res.json();
      }
      else {
        throw res.status;
      }
    })
    .then(res => setOrders(res))
    .catch(err => {
      throw err;
    });
  }, []);

  return [orders, setOrders];
}
