import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from "@angular/router";
import { DietsService } from '../api/diets.service';
import { RegistrationService } from '../../registration/api/registration.service';
import { ProducerDietDTO } from '../api/producerDietDTO';

@Component({
  selector: 'app-producer-diets',
  templateUrl: './producerDiets.component.html',
  styleUrls: ['./producerDiets.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class ProducerDietsComponent implements OnInit {
  constructor(private TitleService: Title, private router: Router, private dietsService: DietsService, private registrationService: RegistrationService) {
    this.TitleService.setTitle("Lista udostępnionych diet");
  }


  public diets: ProducerDietDTO[];

  ngOnInit(): void {
    this.resolveDiets();
  }

  resolveDiets() {
    this.dietsService.getProducerDiets()
      .then((data) => {
        this.diets = (data as ProducerDietDTO[]);
      });
  }

  deleteDiet(dietId: string) {
    this.dietsService.deleteDiet(dietId)
      .then(this.resolveDiets)
      //.catch(() => { alert("Server side error occured") })
      ;
  }

  goToDiet(dietId: number) {
    this.router.navigate(["producer/diets", dietId]);
  }

  createDiet() {
    this.router.navigate(["producer/diets", 0]);
  }
}
