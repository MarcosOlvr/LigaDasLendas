<script setup>
</script>

<template>
  <header>
      <nav class="navbar navbar-expand-lg navbar-dark bg-dark m-2">
        <div class="container">
          <router-link to="/" class="navbar-brand">Liga das Lendas</router-link>
          <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
            <div class="navbar-nav">
              <router-link to="/" class="nav-item nav-link">Inicio</router-link>
              <router-link to="/champions" class="nav-item nav-link">Campeões</router-link>
            </div>
          </div>
        </div>
      </nav>
  </header>
<hr>

  <main class="container-fluid">
    <router-view></router-view>
  </main>
</template>