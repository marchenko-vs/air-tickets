import React from "react";

export interface TicketModel {
    id: number;
    flightId: number;
    orderId: number;
    row: number;
    place: string;
    class: string;
    refund: boolean;
    price: number;
}

export default TicketModel;
