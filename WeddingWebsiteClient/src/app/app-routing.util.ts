import { Routes } from '@angular/router';

export function getRouteComponents(routes: Routes, routeComponents: any[] = []) {
  routes.forEach(route => {
    if (route.component) {
      routeComponents.push(route.component);
    }

    if (route.children) {
      getRouteComponents(route.children, routeComponents);
    }
  });

  return routeComponents;
}