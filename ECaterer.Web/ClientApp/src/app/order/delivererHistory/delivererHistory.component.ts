import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from "@angular/router";
import { DelivererHistoryDTO } from '../api/orderDTO';
import { OrderService } from '../api/order.service';

@Component({
  selector: 'app-deliverer-history',
  templateUrl: './delivererHistory.component.html',
  styleUrls: ['./delivererHistory.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class DelivererHistoryComponent implements OnInit {
  constructor(private TitleService: Title, private router: Router, private ordersService: OrderService) {
    this.TitleService.setTitle("Historia zamówień");
  }


  public orders: DelivererHistoryDTO[];

  ngOnInit(): void {
    this.resolveOrders();
  }

  resolveOrders() {
    this.ordersService.getDelivererHistory()
      .then((data) => {
        this.orders = (data as DelivererHistoryDTO[]);
        this.orders.forEach(o => o.deliveryDate = new Date(o.deliveryDate));
      });
  }
}
