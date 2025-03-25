import { useEffect, useState } from "react";
import OrderService from "../services/OrderService";

export type PriceHookType = [
  price: number, 
  setPrice: (value: any) => void
]

export default function useGetOrderSum(baseUrl: string, jwt: string, orderId: number): PriceHookType {
  const [price, setPrice] = useState(0.0);

  const orderService: OrderService = new OrderService(baseUrl, jwt);

  useEffect(() => {
    orderService.getSumByOrderid(orderId)
    .then(res => {
      if (res.ok) {
        return res.json();
      }
      else {
        throw res.status;
      }
    })
    .then(res => setPrice(res))
    .catch(err => {
      throw err;
    });
  }, []);

  return [price, setPrice];
}
