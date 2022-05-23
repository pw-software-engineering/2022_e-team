import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from "@angular/router";
import { DelivererOrderDTO } from '../api/orderDTO';
import { OrderService } from '../api/order.service';

@Component({
  selector: 'app-deliverer-orders',
  templateUrl: './delivererOrders.component.html',
  styleUrls: ['./delivererOrders.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class DelivererOrdersComponent implements OnInit {
  constructor(private TitleService: Title, private router: Router, private ordersService: OrderService) {
    this.TitleService.setTitle("Lista zamówień");
  }


  public orders: DelivererOrderDTO[];

  ngOnInit(): void {
    this.resolveOrders();
  }

  resolveOrders() {
    this.ordersService.getDelivererOrders()
      .then((data) => {
        this.orders = (data as DelivererOrderDTO[]);
      });
  }

  deliverOrder(orderNumber: string) {
    this.ordersService.deliverOrder(orderNumber)
      .then(this.resolveOrders);
  }
}
