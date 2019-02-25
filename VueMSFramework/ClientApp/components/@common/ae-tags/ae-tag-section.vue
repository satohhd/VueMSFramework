<template>
    <ae-dynamic-section :page="page" :section="field.name" :title="field.caption" :viewModelClassName="field.assemblyQualifiedName" @setBreadcrumb="setBreadcrumb" @teller="listener"></ae-dynamic-section>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {

        },
 
    }

</script>
<style scoped>
</style>
