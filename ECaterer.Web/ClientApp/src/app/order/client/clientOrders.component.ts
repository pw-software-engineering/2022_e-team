import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from "@angular/router";
import { ClientOrderDTO, MakeComplaintDTO } from '../api/orderDTO';
import { OrderService } from '../api/order.service';

@Component({
  selector: 'app-client-orders',
  templateUrl: './clientOrders.component.html',
  styleUrls: ['./clientOrders.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class ClientOrdersComponent implements OnInit {
  constructor(private TitleService: Title, private router: Router, private ordersService: OrderService) {
    this.TitleService.setTitle("Lista zamówień");
  }

  public complaintModel: MakeComplaintDTO = {
    orderNumber: "",
    description: ""
  };

  public newComplaintShown = false;

  public orders: ClientOrderDTO[];

  ngOnInit(): void {
    this.resolveOrders();
  }

  resolveOrders() {
    this.ordersService.getClientOrders()
      .then((data) => {
        this.orders = (data as ClientOrderDTO[]);
      });
  }

  openComplaint(orderNumber: string) {
    this.complaintModel.orderNumber = orderNumber;
    this.newComplaintShown = true;
  }

  closeNewComplaint() {
    this.newComplaintShown = false;
  }

  makeComplaint() {
    this.ordersService.makeComplaint(this.complaintModel)
      .then(this.resolveOrders.bind(this))
      .then(() => {
        this.newComplaintShown = false;
      });
  }

  cancelComplaint(orderNumber: string) {
    this.ordersService.cancelComplaint(orderNumber)
      .then(this.resolveOrders.bind(this));
  }
}
