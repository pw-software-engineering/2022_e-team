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
