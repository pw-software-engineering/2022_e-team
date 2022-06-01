import { addressDto } from "../../registration/api/registrationDtos";

export interface OrderDTO {
  startDate: Date;
  endDate: Date;
  comment: string;
  dietIds: string[];
  address: addressDto;
};

export interface DelivererOrderDTO {
  orderNumber: string;
  address: string;
  phone: string;
  comment: string;
}

export interface ProducerOrderDTO {
  orderNumber: string;
  orderDate: Date;
  status: string;
}

export interface PreviewOrderDTO {
  orderNumber: string;
  dietNames: string[];
  cost: number;
  status: string;
  address: string;
  phone: string;
  orderDate: Date;
  deliverDate: Date;
  comment: string;
  hasComplaint: boolean;
}

export interface ComplaintDTO {
  clientName: string;
  description: string;
  complaintDate: Date;
  status: string;
}
