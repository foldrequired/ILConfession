/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ConfessionComponent } from './confession.component';

describe('ConfessionComponent', () => {
  let component: ConfessionComponent;
  let fixture: ComponentFixture<ConfessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
