import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

  // placed grid here in order to show how kendo works
  // TODO: remove fast
export class HomeComponent implements OnInit {

  public gridItems: any;

    ngOnInit(): void {
      this.gridItems = [{
        field1: "coca",
        field2: "cola"
      },
      {
        field1: "cheese",
        field2: "burger"
      },
      {
        field1: "pepsi",
        field2: "fanta"
      }]
    }
}
