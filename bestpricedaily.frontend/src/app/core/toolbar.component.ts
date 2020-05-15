import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { getCartCount, CartState } from '../cart/cart.store';

@Component({
  selector: 'app-toolbar',
  templateUrl:'./toolbar.component.html',
  styles: [`
    mat-toolbar {
      height: 40px;
      display: flex;
      justify-content: space-between;
    }
    :host { 
      position: sticky;
      top: 0;
      display: block; 
      z-index: 100; 
    }   
  `]
})
export class ToolbarComponent implements OnInit {
  total$ = this.store.pipe(select(getCartCount));
  constructor(private store: Store<CartState>) { }

  ngOnInit(): void {
  }

}
