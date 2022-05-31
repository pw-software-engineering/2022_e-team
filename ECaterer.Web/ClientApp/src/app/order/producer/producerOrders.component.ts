import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from "@angular/router";
import { ProducerOrderDTO } from '../api/orderDTO';
import { OrderService } from '../api/order.service';

@Component({
  selector: 'app-producer-orders',
  templateUrl: './producerOrders.component.html',
  styleUrls: ['./producerOrders.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class ProducerOrdersComponent implements OnInit {
  constructor(private TitleService: Title, private router: Router, private ordersService: OrderService) {
    this.TitleService.setTitle("Lista zamówień");
  }

  public orders: ProducerOrderDTO[];

  ngOnInit(): void {
    this.resolveOrders();
  }

  resolveOrders() {
    this.ordersService.getProducerOrders()
      .then((data) => {
        this.orders = (data as ProducerOrderDTO[]);
      });
  }

  previewOrder(orderNumber: string) {
    this.router.navigate(["producer/orders", orderNumber]);
  }
}
