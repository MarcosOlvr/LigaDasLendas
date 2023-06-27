import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    component: () => import("../components/SearchSummoner.vue"),
  },
  {
    path: '/champions',
    component: () => import("../components/AllChamps.vue"),
  },
  {
    path: '/champion/:champName',
    name: 'champion',
    component: () => import("../components/Champion.vue"),
    params: true
  },
  {
    path: '/runes',
    component: () => import("../components/Runes.vue"),
  },
  {
    path: '/rune/:runeName',
    name: "rune",
    component: () => import("../components/Rune.vue"),
    params: true
  },
  {
    path: '/spells',
    component: () => import("../components/spells.vue"),
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router