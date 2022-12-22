import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent {
  isCollapsed = true;
  constructor(private router: Router) {}

  showMenu(): boolean {
    var url = this.router.url;

    if (url === '/user/login') {
      return false;
    }
    if (url === '/user/register') {
      return false;
    }

    return true;
  }
}
