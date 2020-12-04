import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  deslogado = true;
  /**
   *
   */
  constructor(private router: Router) {
    
    //#temp
    if (localStorage.getItem('user token')) {
        this.deslogado = false;
    }
    
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/'])
  }
}
