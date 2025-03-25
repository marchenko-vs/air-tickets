import { useEffect, useState } from "react";
import OrderService from "../services/OrderService";

export type PriceHookType = [
  status: number,
  price: number, 
  setPrice: (value: any) => void
]

export default function useGetCurrentOrderSum(baseUrl: string, jwt: string): PriceHookType {
  const [price, setPrice] = useState(0.0);
  const [status, setStatus] = useState(200);

  const orderService: OrderService = new OrderService(baseUrl, jwt);

  useEffect(() => {
    orderService.getCurrentSum()
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
      setStatus(err);
    });
  }, []);

  return [status, price, setPrice];
}
