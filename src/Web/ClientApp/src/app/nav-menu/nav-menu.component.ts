import { Component } from '@angular/core';
import { TokenService } from '../services/token.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  constructor(public readonly tokenService: TokenService) {
  }
}
