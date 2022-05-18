import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { GridModule } from '@progress/kendo-angular-grid';

import { Diets } from './diets.component';

describe('DietsList', () => {
  let component: Diets;
  let fixture: ComponentFixture<Diets>;

  beforeAll(function () {
    jasmine.DEFAULT_TIMEOUT_INTERVAL = 999999;
  });

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [Diets],
      imports: [GridModule]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Diets);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
