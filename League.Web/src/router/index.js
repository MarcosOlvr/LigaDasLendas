import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    component: () => import("../components/SearchSummoner.vue"),
  },
  {
    path: '/champions',
    component: () => import("../components/AllChamps.vue"),
  }
]
const router = createRouter({
  history: createWebHistory(),
  routes
})
export default router